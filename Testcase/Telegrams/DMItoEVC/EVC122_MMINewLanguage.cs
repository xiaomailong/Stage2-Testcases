using System;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet will be sent when the driver has selected a new language with the language button.
    /// </summary>
    public static class EVC122_MMINewLanguage
    {
        private static SignalPool _pool;
        const string baseString = "DMI->ETCS: EVC-122 [MMI_NEW_LANGUAGE]";

        /// <summary>
        /// Initialise EVC-122 MMI_New_Language.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1NewLanguage.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewLanguage.Value = 0x0001;
        }

        public static void CheckNidLanguage()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewLanguage.Value = 0x00;

            // Check if telegram received flag has been set.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewLanguage.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // If check passes
                if (_pool.SITR.CCUO.ETCS1NewLanguage.MmiNidLanguage.Value.Equals(MMI_NID_LANGUAGE))
                {
                    _pool.TraceReport(string.Format("{0} - [MMI_NID_LANGUAGE] = {1}", baseString, MMI_NID_LANGUAGE) + Environment.NewLine +
                                      "Result: PASSED.");
                }
                // Else display the real value extracted from EVC-122

                else
                {
                    _pool.TraceError(string.Format("{0} - [MMI_NID_LANGUAGE] = ", baseString) +
                                     _pool.SITR.CCUO.ETCS1NewLanguage.MmiNidLanguage.Value +
                                     Environment.NewLine + "Result: FAILED");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString);
            }
        }

        /// <summary>
        /// Language code.
        /// 
        /// Values:
        /// 255 = "'Language unknown'"
        /// 
        /// Note: No mapping between value and language is required for ETC, only storage and transfer of the value.
        /// </summary>
        public static uint MMI_NID_LANGUAGE { get; set; }
    }
}
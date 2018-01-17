﻿using System;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent sporadically from DMI when the driver has submitted data in the 'Set VBC' window.
    /// </summary>
    public static class EVC118_MMINewSetVbc
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static uint _mVDCCode;

        static string baseString0 = "DMI->ETCS: EVC-118 [MMI_NEW_SET_VBC]";

        /// <summary>
        /// Initialise EVC-118 MMI_New_Set_Vbc telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewSetVbc.Value = 0x0001;
            _pool.SITR.SMDStat.CCUO.ETCS1NewSetVbc.Value = 0x00;
        }

        public static void CheckPacketContent()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewSetVbc.Value = 0x00;

            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewSetVbc.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check all static fields
                _checkResult = _pool.SITR.CCUO.ETCS1NewSetVbc.MmiMVbcCode.Value.Equals(MMI_M_VBC_CODE) &
                               _pool.SITR.CCUO.ETCS1NewSetVbc.MmiMButtons.Value.Equals((byte) MMI_M_BUTTONS);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(baseString0 + Environment.NewLine +
                                      "MMI_M_VBC_CODE = " + MMI_M_VBC_CODE + Environment.NewLine +
                                      "MMI_M_BUTTONS = \"" + MMI_M_BUTTONS + "\"" + Environment.NewLine +
                                      "Result: PASSED.");
                }
                // Else display the real value extracted from EVC-118
                else
                {
                    _pool.TraceError(baseString0 + Environment.NewLine +
                                     "MMI_M_VBC_CODE = \"" + _pool.SITR.CCUO.ETCS1NewSetVbc.MmiMVbcCode.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BUTTONS = \"" +
                                     Enum.GetName(typeof(Variables.MMI_M_BUTTONS_VBC),
                                         _pool.SITR.CCUO.ETCS1NewSetVbc.MmiMButtons.Value) + Environment.NewLine +
                                     "Result: FAILED!");
                }
            }
            // EVC-118 could not be received
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString0);
            }
        }

        /// <summary>
        /// Virtual Balise Cover code.
        /// 
        /// Bits:
        /// 0..9 = "NID_C"
        /// 10..15 = "NID_VBCMK"
        /// 16..23 = "T_VBC"
        /// 24..31 = "spare"
        /// </summary>
        public static uint MMI_M_VBC_CODE { get; set; }

        /// <summary>
        /// Only MMI Buttons used in EVC-118. 
        /// 
        /// Values:        
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"       
        /// 253 = "BTN_ENTER_DELAY_TYPE"
        /// 254 = "BTN_ENTER"
        /// 255 = "no button"
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static Variables.MMI_M_BUTTONS_VBC MMI_M_BUTTONS { get; set; }

        public static uint Get_M_VBC_CODE
        {
            get
            {
                // Reset telegram received flag in RTSim
                _pool.SITR.SMDStat.CCUO.ETCS1NewSetVbc.Value = 0x00;

                if (_pool.SITR.SMDStat.CCUO.ETCS1NewSetVbc.WaitForCondition(Is.Equal, 1, 20000, 100))
                {
                    _mVDCCode = _pool.SITR.CCUO.ETCS1NewSetVbc.MmiMVbcCode.Value;
                    return _mVDCCode;
                }
                else
                {
                    DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString0);
                    return 0;
                }
            }
        }
    }
}